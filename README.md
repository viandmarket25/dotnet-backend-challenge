# Dotnet Backend Engineer Challenge

Backend task demonstrating Understanding of Software Architecture using Martha's Book Library

# Table of contents

1. [Introduction](#introduction)
2. [Book Borrowing and Reservation process in old system](#paragraph1)
3. [Feature](#subparagraph1)
4. [Tools and Development Kits Required](#subparagraph1)
5. [Tech Stack](#paragraph2)
6. [System Design](#paragraph2)

## Introduction

Martha owns a local library, her customers often complain about coming to the book store and not finding the books they came to borrow. To prevent this, customers currently call the library and ask if the books they are looking for are available. Typically, over the call.

## Book Borrowing and Reservation process in old system

The customer specifies the name of the book
Martha checks her library for the book
If it's available, the customer has the option to reserve it for the next 24 hours until they've come to the library to collect it.
If the book is reserved, another customer cannot reserve or borrow it.
If the book has been borrowed by another customer, Martha lets the enquiring customer know when the book would be returned to the library

In your implementation, take into account the following key details:

Authentication & Authorization: Since the application will most likely store customer data, some security constraints must be enforced on who has access to the data.
API Documentation: The application should run on any device, documenting the API would be important as it would allow any frontend engineer to make the app available to customers using different devices.

## Features

- User Management
- Book Management
- Genre Management
- Shelf Management
- Notification

## Tools and Development Kits Required

- .Net Core 6 sdk
- Visual Studio Ide or Visual Studio Code
- Mysql Server (mysql, mariadb)

## Tech Stack

**Server:** .Net Core 6, VsCode, Mysql Server, Jwt(Authentication Implementation), Role Based Authorization

## Run Locally

Clone the project

```bash
  git clone https://github.com/viandmarket25/dotnet-backend-challenge.git
```

Create database "dotnet_library"

CREATE DATABASE `dotnet_library`;

Import dotnet_libray.sql file in config folder to dotnet_libray database.

```
  Set Database Connection in config MysqlConnectionPipe.cs at line 13 in
 public string InitMysqlConnectionPipe ()
  {
    this.connstring = string.Format("Server=localhost; database={0}; UID=root; password=123456; SslMode = none",      "dotnet_library");
    this.connection = new MySqlConnection(this.connstring);
    this.connection.Open();
    return "";
  }
```

Go to the project directory

```bash
  cd libray_api
```

build dependencies

run to test (starts server)

```run to test (starts server)
dotnet build
```

run to test (starts server)

```run to test (starts server)
dotnet run
```

run to develop (starts server while listening for changes)

```run to develop  (starts server while listening for changes)
dotnet watch
```

goto url/swagger/index.html to test Api

```run
  visit http://localhost:7215/swagger/index.html
```

## Screenshots

## System Design

Erd (Entity Relationship Diagram)

<img width="948" alt="Screenshot 2023-02-09 at 16 18 44" src="https://user-images.githubusercontent.com/45737118/217926350-620af5d0-dd31-45a3-8b28-4151a2eaabd6.png">

Models
<img width="1071" alt="Screenshot 2023-02-10 at 01 29 48" src="https://user-images.githubusercontent.com/45737118/217926570-ff779c3d-fb04-4a5b-b018-5443852ea523.png">

<img width="937" alt="Screenshot 2023-02-09 at 00 17 24" src="https://user-images.githubusercontent.com/45737118/217926521-ac0201a9-80a6-45c2-a969-e99db64c7ac2.png">

Api Routes

<img width="1071" alt="Screenshot 2023-02-10 at 01 28 45" src="https://user-images.githubusercontent.com/45737118/217926603-b3b6bf73-926f-4848-b94e-f6354a56d07b.png">

<img width="1071" alt="Screenshot 2023-02-10 at 01 29 03" src="https://user-images.githubusercontent.com/45737118/217926622-bd0755ab-d242-4588-bc14-c2f5ee078221.png">

<img width="1071" alt="Screenshot 2023-02-10 at 01 29 13" src="https://user-images.githubusercontent.com/45737118/217926632-255d35e3-c92d-47d5-bfb9-4a3b0dd4fbed.png">

<img width="1071" alt="Screenshot 2023-02-10 at 01 29 26" src="https://user-images.githubusercontent.com/45737118/217926656-b17708d8-5115-4323-8abb-bf7d877b2e9b.png">

<img width="1071" alt="Screenshot 2023-02-10 at 01 29 36" src="https://user-images.githubusercontent.com/45737118/217926677-0e4435f7-09c3-4875-86a9-f175e170195b.png">

<img width="1071" alt="Screenshot 2023-02-10 at 01 29 48" src="https://user-images.githubusercontent.com/45737118/217926723-75bfbd7f-8fe1-460a-a3f6-46f2d4b08cf6.png">

## License

[MIT](https://choosealicense.com/licenses/mit/)
