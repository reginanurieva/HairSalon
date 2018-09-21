# "Hair Salon"

#### Epicodus independent project in C#, 9.21.2018

#### By Regina Nurieva

## Description

Program in C# that allows the owner to add stylists, and for each stylist, add clients. The stylists work independently, so each client only belongs to a single stylist.

## Specs
1. The user enter a Stylist.
  * Output Example: "Please enter the name"
  * Input Example: "Elvira Garcia."
2. The user enters clients that belongs to the particular stylist.
  * Input Example:"Add Clients"
  * Output Example: "John Doe".

## Setup/Installation Requirements

* Clone this repository from https://github.com/reginanurieva/HairSalon.git
* Open up in the console with the following command:
```
cd HairSalon.Solutions
```
* Navigate to the HairSalon folder:
```
cd HairSalon
```
* Run the following commands:
```
dotnet restore
```

```
dotnet build
```

```
dotnet run
```

## To access MySQL Database you need to run following commands:

* _CREATE DATABASE hair_salon;_
* _USE hair_salon;_
* _CREATE TABLE stylists (stylist_name VARCHAR (255), id serial PRIMARY KEY);_
* _CREATE TABLE clients (client_name VARCHAR (255), stylist_id INT, id serial PRIMARY KEY);_

## Support and contact details

Regina Nurieva, reggi.nurieva@gmail.com

## Technologies Used

C#

Git

Github

Atom

.NET Core 1.1

MySql

PHP MyAdmin

MAMP

### License

This software is licensed under the MIT license.

Copyright (c) 2018 **Regina Nurieva**
