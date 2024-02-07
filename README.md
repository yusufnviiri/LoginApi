## Login API
    This project is designed to authenticate and authorise a user using JWT token
## Major Cornerstones of the project
    - Create a user with password and username
	- View Users in database by submitting a user token
	- Login into the system where a JWT token is issued
    - User can change password at will
    - User interractions with the system stored in AuditTrail table.
    
## Built with
    - C#
    - Entity Framework Core 6
    - MySQL    
    - JWT bearer Authentication Nuget
    - Microsoft.Identity Nuget
## Requirements
    - Knowledge of C#
    - Entity Framework core 6
    - Code Editor like Visual Studio
    - MySQL
    - MySQL management studio
    - DotNet framework core 6
    - Postman API Tester to test User endpoints
## Getting Started
    - git init
    - Clone Repository (https://github.com/yusufnviiri/LoginApi.git)
    - cd LoginAPi
    - update nuggets
    - Run Project
    - Enjoy
## API End Points
## For secured endpoints, copy a token recieved from a reponse after Login and add it in the header as bearer token  
    -  https://localhost:5001/api/AuditTrails (view user logs) - get
    -  https://localhost:5001/api/ChangePassword (change Password) - post  object format {  "passwordChangeId": 0,  "userName": "string",  "oldPassword": "string",  "newPassword": "string",  "confirmPassword": "string"}
    -  https://localhost:5001/api/Login (login user) - post  object format {  "userId": 0,  "userName": "string",  "password": "string"}
    -  https://localhost:5001/api/Users (Register user ) - post  object format {  "userId": 0,  "userName": "string",  "password": "string"}
    -  https://localhost:5001/api/Users (View users)  - get 
## 👤 Author
## 👤 Yusuf Nviiri

    - GitHub: [@yusufnviiri](https://github.com/yusufnviiri)
    - Twitter: [@YNviiri](https://twitter.com/YNviiri)
    - LinkedIn: [yusufnviiri](https://www.linkedin.com/in/yusufnviiri)    

### 🤝 Contributing

    - Contributions,code reviews and feature requests are all welcome

### Show your support

    - Give a ⭐️ if you liked this project