# Dotnet Backend Engineer Challenge

Backend task demonstrating Understanding of Software Architecture using Martha's Book Library

# Table of contents

1. [Introduction](#introduction)
2. [Book Borrowing and Reservation process in old system](#paragraph1)
3. [Feature](#subparagraph1)
4. [Tech Stack](#paragraph2)

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

## Tech Stack

**Server:** .Net Core 6, VsCode, Mysql Server, Jwt(Authentication Implementation), Role Based Authorization

## Run Locally

Clone the project

```bash
  git clone https://github.com/viandmarket25/dotnet-backend-challenge.git
```

Go to the project directory

```bash
  cd libray_api
```

Install dependencies

```run to test (starts server)
dotnet run
```

```run to develop  (starts server while listening for changes)
dotnet watch
```

## License

[MIT](https://choosealicense.com/licenses/mit/)

## Screenshots

## Tech Stack

**Server:** Node, Express

## Run Locally

Clone the project

```bash
  git clone https://link-to-project
```

Go to the project directory

```bash
  cd my-project
```

Install dependencies

```bash
npm install
```

Start the server

```bash
npm run start
```

## License

[MIT](https://choosealicense.com/licenses/mit/)
