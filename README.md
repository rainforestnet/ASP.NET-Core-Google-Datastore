# ASP.NET Core 2.0 and Google Datastore (NoSQL)
This project was created to serve one simple purpose.
It is simply to demonstrate how to perform CRUD Google NoSQL Datastore with ASP.net Core 2.0.
In fact, it is just more for my own reference for future works

## Language
C#

## Database 
Google Datastore
https://cloud.google.com/datastore/docs/concepts/overview

## Projects of the Solution
1. __WmNosql.Proxy__
Google Datastore being the database this application, we won't able to use Entity Framework or ADO.NET to interact retrieve and store data.
Hence, I chose to create a proxy class for each table (it is called Kind in Google Datastore context).
This project is the collection of all proxy classes that to be mapped into Datastore.

2. __WmNosql.Cmd__
This is just a command line program to invoke proxy classes merely for testing.

3. __WmNosql.RazorPage__
This is the ASP.NET web front end by using Razor Page to perform CRUD by calling proxy classes.

## How to Interact with Google Datastore (setup guide for Windows 10)
1. Sign up to Google Cloud https://cloud.google.com. Google Datastore is one of the offerring under Google Cloud Platform.

2. Create Service Account
- Account name: \*droid101\* (just example)
- Role: Datastore -> cloud datastore owner

3. Download json private key file keep it in any folder.
i.e C:\GCloudKey\wm-nosql-a4fcc17e9390.json

4. Setup environment
a. Control Panel > System > Advanced System Settings > Environment Variables
b. Set Variable
c. Variable Name: GOOGLE_APPLICATION_CREDENTIALS
d. Variable Value: C:\GCloudKey\wm-nosql-a4fcc17e9390.json

5.Restart machine (very important)

6. Configure GCloud
Use Powershell or Command Prompt of Windows.
- gcloud config list (View current logged in accounts and default project in Google cloud)
- gcloud auth login (Sign In to a Google account)
- gcloud config set account \*google account email\*
- gcloud config set project \*project name\*

## Datastore Indexes management

Without Indexes, you almost won't be able to query data from Google Datastore. So learn about Index of Datastore here.
https://cloud.google.com/datastore/docs/concepts/indexes

1. Create Indexes and Deploy
Powershell or Command Prompt
> gcloud app deploy "C:\Users\user\Documents\Projects\WMNoSQL\WmNosql.Proxy\index.yaml"
OR
> gcloud datastore create-indexes C:\Users\user\Documents\Projects\WMNoSQL\WmNosql.Proxy\index.yaml

If you were to delete Indexes
Powershell or Command Prompt
> gcloud datastore cleanup-indexes "C:\Users\user\Documents\visual studio 2017\Projects\WMNoSQL\WmNosql.Proxy\index.yaml"
