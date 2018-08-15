# ASP.NET Core 2.0 and Google Datastore (NoSQL)
This project was created to serve one simple purpose.
It is simply to demonstrate how to perform CRUD Google NoSQL Datastore with ASP.net Core 2.0.

## Language
C#

## Database 
Google Datastore
https://cloud.google.com/datastore/docs/concepts/overview

## Projects
1. WmNosql.Proxy
Google Datastore being the database this application, we won't able to use Entity Framework or ADO.NET to interact retrieve and store data.
Hence, I chose to create a proxy class for each table (it is called Kind in Google Datastore context).
This project is the collection of all proxy classes that to be mapped into Datastore.

2. WmNosql.Cmd
This is just a command line program to invoke proxy classes merely for testing.

3. WmNosql.RazorPage
This is the ASP.NET web front end by using Razor Page to perform CRUD by calling proxy classes.

## How to Google Datastore

1. Create Service Account
--------------------------
account name: droid101
role: Datastore -> cloud datastore owner

download private key in json format
wm-nosql-a4fcc17e9390.json

2. Control Panel > System > Advanced System Settings > Environment Variables
----------------------------------------------------------------------------
Set Variable
Variable Name: GOOGLE_APPLICATION_CREDENTIALS
Variable Value: C:\GCloudKey\wm-nosql-a4fcc17e9390.json

restart machine (very important)

3.Configure GCloud
-------------------
Powershell or Command Prompt

gcloud config list
gcloud auth login
gcloud config set account <google account email>
gcloud config set project <project name>

4. Create Indexes and Deploy
----------------------------
Powershell
> gcloud app deploy "C:\Users\user\Documents\visual studio 2017\Projects\WMNoSQL\WmNosql.Proxy\index.yaml"
OR
> gcloud datastore create-indexes C:\Users\user\Documents\visual studio 2017\Projects\WMNoSQL\WmNosql.Proxy\index.yaml

    
If Delete Indexes
-----------------
Powershell
> gcloud datastore cleanup-indexes "C:\Users\user\Documents\visual studio 2017\Projects\WMNoSQL\WmNosql.Proxy\index.yaml"
