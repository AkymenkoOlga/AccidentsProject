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
df = pandas.read_csv('data\CRASH_2017.csv')

# get a data frame with selected columns
meta_fields = ['DEC_LAT', 'DEC_LONG', 'AUTOMOBILE_COUNT', 'BICYCLE_COUNT', 'FATAL_COUNT', 'MOTORCYCLE_COUNT',
               'ILLUMINATION', 'CRN']
date_fields = ['CRASH_YEAR', 'CRASH_MONTH']

selected_fields = meta_fields.extend(date_fields)

df_selected = df[meta_fields].to_dict(orient='records')

for each in df_selected:
    accident = {
      "date": str(datetime.datetime(year=int(each['CRASH_YEAR']), month=int(each['CRASH_MONTH']), day=1)),
      "location": {
        "coordinates": {
          "lat": each['DEC_LAT'],
          "lon": each['DEC_LONG']
        }
      },
      "name": each['CRN'],
      "tags": []
    }

    if not is_valid_accident(accident):
        print('invalid accident', accident['name'])
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
    else:
        accident['tags'].extend(['night'])

    send_accident(json.dumps(accident))
