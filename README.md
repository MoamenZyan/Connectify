# Connectify

Connectify is a modern messaging platform that enables users to message each other in real-time, send photos, and create groups. It offers a sleek and responsive UI, ensuring a smooth messaging experience. Built with modern technologies, it is scalable and efficient for real-time communication.

## Features

- Real-time messaging between users
- Photo sharing capabilities
- Group messaging support
- Modern and responsive user interface
- High-performance backend infrastructure
- Scalable storage for media (photos) using S3

## Tech Stack

### Frontend

- **NEXT.js v14**: Server-side rendering and static site generation for performance optimization
- **REACT.js**: Component-based user interface development

### Backend

- **ASP.NET Core v8**: High-performance and scalable backend framework
- **Hangfire**: Background job scheduling
- **Redis**: Caching for optimized performance and message queueing
- **S3 Bucket**: Storage solution for handling media files (photos)
- **SQL Server**: Main Database to store application entities info

## Installation

To run this project locally, follow these steps:

### Prerequisites

Make sure you have the following installed:

- **Node.js** (for frontend)
- **.NET Core SDK** (for backend)
- **Redis** (for caching)
- **Hangfire Dashboard** (optional: for monitoring background jobs)
- **SQL Server** (for storing info)
- **S3 Bucket** (for storing photos)

### Steps

1. **Clone the repository**

  ```bash
  git clone https://github.com/yourusername/connectify.git
  cd connectify
  ```

2. **Install dependencies**
   - For frontend
   
    ```bash
    cd frontend
    npm install
    ```
   - For backend
  
   ```bash
   cd backend
   dotnet restore
   ```
3. **Set up environment variables**
   - For backend
     ```bash
     SendGridConfigurations__ApiKey=<Your SendGrid Api Key>
     SendGridConfigurations__FromEmail=<Your SendGrid Sending Email>

     JWTConfigurations__Issuer=<Your Token Issuer>
     JWTConfigurations__Audience=<Your Token Audience>
     JWTConfigurations__SigningKey=<Your Token Signing Key>

     AWS__AccessKey=<Your AWS Access Key>
     AWS__SecretAccessKey=<Your AWS Secret Key>
     AWS__Region=<Your Bucket Region>
     AWS__BucketName=<Your S3 Bucket Name>
     ```
4. **Run the Application**
   - For backend
     ```bash
     cd backend
     dotnet run
     ```
   - For frontend
     ```bash
     cd frontend
     npm run dev
     ```
5. **Access the application**
   - Open your browser and go to
     ```bash
     http://localhost:3000
     ```

## Usage
- Messaging: Users can send and receive real-time messages.
- Photo Sharing: Users can upload and share photos in conversations.
- Group Chats: Users can create or join groups to message multiple people at once.

## Screenshots

![homepage-connectify](https://github.com/user-attachments/assets/80d68938-ccda-4a20-97a9-dd580f10bb68)
![hompage v2](https://github.com/user-attachments/assets/2fb4716f-ddcc-4366-a716-be02518619c8)
![login-page](https://github.com/user-attachments/assets/414df5c2-e779-49c2-9391-90502acec508)
![signup-page](https://github.com/user-attachments/assets/643d60ee-1a1d-45d9-bc37-a9363e5b3760)
![friend request notification](https://github.com/user-attachments/assets/3344fa5a-e506-405d-9720-0ceaf9e6fcb5)
![friend accepted notification](https://github.com/user-attachments/assets/0e3ffbdf-c1f5-47a1-af58-eda46500300d)
![chat-page](https://github.com/user-attachments/assets/8eb70a50-35a7-4e28-95ab-053c1293725b)


## Conclusion
Connectify is designed to provide users with a seamless and modern messaging experience, combining real-time communication, multimedia sharing, and group functionality in a scalable and efficient platform. Built using cutting-edge technologies, it ensures high performance, scalability, and a user-friendly interface. With its robust backend infrastructure and sleek frontend design, Connectify is an ideal solution for modern messaging needs. We are continuously working to improve the
