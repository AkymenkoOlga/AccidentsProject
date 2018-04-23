import pandas
import requests
import datetime
import json
import math


def is_valid_accident(accident):
    return not math.isnan(accident['location']['coordinates']['lat']) or \
           not math.isnan(accident['location']['coordinates']['lon'])


def send_accident(accident):
    session = requests.Session()
    session.headers.update({'Content-Type': 'application/json'})

    r = session.post('http://localhost:55475/api/accidents', data=accident)
    print(r.status_code, accident)


# get a data frame from csv file
data = pandas.read_csv('data\CRASH_2017.csv')

# select columns (CRN - Crash Record Number, is a unique key of an accident)
selected_fields = ['DEC_LAT', 'DEC_LONG', 'AUTOMOBILE_COUNT', 'BICYCLE_COUNT', 'FATAL_COUNT', 'MOTORCYCLE_COUNT',
                    'ILLUMINATION', 'CRN', 'CRASH_YEAR', 'CRASH_MONTH']

# get a data frame with selected columns
data_selected = data[selected_fields].to_dict(orient='records')

# construct accidents and send to AccidentsProject API
for each in data_selected:
    accident = {
      "date": str(datetime.datetime(year=int(each['CRASH_YEAR']), month=int(each['CRASH_MONTH']), day=1)),
      "location": {
        "coordinates": {
          "lat": each['DEC_LAT'],
          "lon": each['DEC_LONG']
        }
      },
      "externalId": each['CRN'],
      "tags": []
    }

    if not is_valid_accident(accident):
        print('invalid accident', accident['externalId'])
        continue

    # add tags
    if each['AUTOMOBILE_COUNT']:
        accident['tags'].extend(['automobile'])
    if each['BICYCLE_COUNT']:
        accident['tags'].extend(['bicycle'])
    if each['FATAL_COUNT']:
        accident['tags'].extend(['fatal'])
    if each['MOTORCYCLE_COUNT']:
        accident['tags'].extend(['motorcycle'])

    if each['ILLUMINATION'] == 1:
        accident['tags'].extend(['day'])
    elif each['ILLUMINATION'] != 8 and each['ILLUMINATION'] != 9:
        accident['tags'].extend(['night'])

    send_accident(json.dumps(accident))
