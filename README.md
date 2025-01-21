# Endura Genius
![cover Image](./cover.png)
**Endura Genius** is a cutting-edge web API for managing personalized workout plans, tracking progress, and enhancing fitness journeys with professional coaching.


# Table of Contents

- [Features](#features)
- [Getting Started](#getting-started)
- [Installation](#installation)
- [Usage](#usage)
- [API Endpoints](#api-endpoints)
- [Technologies Used](#technologies-used)
- [Contributing](#contributing)
- [License](#license)
- [Authors](#author)

## **Features**

- **Workout Plans**: Explore a variety of professionally designed general workout plans suitable for different fitness levels and goals, created by expert coaches.
- **User-Created Plans**: Create private workout plans.
- **InBody Results**: Get detailed body composition analysis.
- **Progress Tracking**: Save and review progress (max weights, last played weights, etc.).
- **Custom Workouts**: Generate plans tailored to user performance and history.
- **Leaderboard**: Compete with others by earning points for completing workouts and improving performance.
- **Public Profiles**: Users can choose to make their profiles public, allowing them to share their progress, profile points, and photo with the community.
- **Real-Time Updates**: Stay updated on workout changes from coaches.
- **Comprehensive Unit Testing**: Ensured the reliability of the application with 170+ unit test cases using XUnit, FluentAssertions, FakeItEasy, and Entity Framework Core InMemory.

## **Getting Started**

### Prerequisites
- .NET 8.0 SDK or higher
- SQL Server
- Visual Studio

## **Installation**
1. Clone the repository:
```bash 
git clone https://github.com/your-username/endura-genius.git
```
2. Navigate to the project directory
```bash
cd endura-genius
```
3. Restore dependencies:
```bash
dotnet restore
```
4. Update database settings in `appsettings.json`.
5. Run database migrations:
```bash
dotnet ef database update
```
6. Start the application:
```bash
dotnet run
```
7. First Admin Account:
  The First Admin account is set with 
      - email : ```admin@EnduraGenius.com``` 
      - password : ```Mo123123```.
8. Optional: Use Pre-existing Database Backup for Immediate Testing
    - If you want to test the application immediately, you can restore the database from the provided backup:
        - The backup file is located in the [EnduraGenius.API.DatabaseBackup](./EnduraGenius.API.DatabaseBackup/) directory.
        - Use your preferred database management tool or command line to restore the backup.
        - Ensure the database is restored to match the configuration in the appsettings.json file.
9. Optional : Login Data for Testing
    - The testing login credentials are available in the [EnduraGenius.API.DatabaseBackup/users.txt](./EnduraGenius.API.DatabaseBackup/users.txt) file.
    - Any login data provided in users.txt is only valid for the database backup located in the ./dbbackup directory.
10. Optional: Get a Free Mailbox for Testing
- If you need a mailbox for testing purposes, you can generate one for free using [Ethereal Email](https://ethereal.email/).


## **Usage**
- Use Postman or Swagger UI to test endpoints.
- Examples : Fetch all user available plans
	``` http
	GET /api/Plans
	```

## **API Endpoints**

### **Register New Admin Account**  
- **HTTP Method**: `POST`  
- **Endpoint**: `/api/Admin/Register`  
- **Request Body**:  

```json
{
  "userName": "string",
  "email": "string",
  "password": "string",
  "weightInKg": number,
  "tallInCm": number,
  "age": number,
  "isMale": boolean
}
```
### **Register New User Account**  
- **HTTP Method**: `POST`  
- **Endpoint**: `/api/Auth/Register`  
- **Request Body**:  

```json
{
  "userName": "string",
  "email": "string",
  "password": "string",
  "weightInKg": number,
  "tallInCm": number,
  "age": number,
  "isMale": boolean
}
```

### **Login User**
- **HTTP Method: POST**
- **Endpoint: /api/Auth/Login**
- **Request Body**:
```json
{
  "emailAdress": "string",
  "password": "string"
}
```

### **Forgot Password (Send Reset Link)**
- **HTTP Method: POST**
- **Endpoint: /api/Auth/ForgotPassword**
- **Request Body**:
```json
{
  "email": "string",
  "clientURI": "string"
}
```
### **Forgot Password (Send Reset Token Only)**
- **HTTP Method: POST**
- **Endpoint: /api/Auth/ForgotPassword/tokenOnly**
- **Request Body**:
```json
{
  "email": "string"
}
```

### **Reset User Password**
- **HTTP Method: POST**
- **Endpoint: /api/Auth/ResetPassword**
- **Request Body**:
```json
{
  "email": "string",
  "token": "string",
  "password": "string"
}
```

### **Test Error Logger**
- **HTTP Method: POST**
- **Endpoint: /api/Auth/TestErrorLogger**

### **Post New Inbody for Current User**
- **HTTP Method**: `POST`  
- **Endpoint**: `/api/Inbody`  
- **Request Body**:

```json
{
  "name": "string",
  "activityLevel": "string"
}
```
### **Create a New Plan**  
- **HTTP Method**: `POST`  
- **Endpoint**: `/api/Plans`  
- **Request Body**:  

```json
{
  "name": "string",
  "descreption": "string",
  "workoutsDtos": [
    {
      "workoutId": "guid",
      "reps": number,
      "dayNumber": number,
      "order": number
    }
  ]
}
```
### For additional endpoints, please visit our Swagger documentation.


## **Technologies Used**
- **ASP.NET Core 8.0**: Backend framework.
- **Entity Framework Core**: ORM for database operations.
- **SQL Server**: Database management system.
- **Swagger UI**: API documentation.
- **XUnit**: Unit testing framework.
- **FluentAssertions**: Library for more readable and expressive assertions.
- **FakeItEasy**: Mocking library for easier testing.
- **Entity Framework Core InMemory**: Database provider for testing.
- **AutoMapper**: Library for object-to-object mapping, simplifying DTO conversion and model mapping.
- **MailKit**: Cross-platform mail client library for sending and receiving emails.
- **ASP.NET Core Authentication**: Provides robust authentication and authorization mechanisms for securing APIs.
- **Serilog**: Structured logging framework for easy log management, querying, and monitoring.

## **Contributing**
Contributions are welcome! Please follow these steps:
1. Fork the repository.
2. Create a new branch:  
    ```bash
    git checkout -b feature-name
    ```
3. Commit your changes:
    ```bash
    git commit -m "Add feature-name"
    ```
4. Push to the branch:
    ```bash
    git push origin feature-name
    ```
5. Create a pull request.

## **License**
This project is licensed under the MIT License. See the [LICENSE](LICENSE.txt) file for details.

## **Author**

- [Mohamed Alaga](https://github.com/MohamedAlaga)