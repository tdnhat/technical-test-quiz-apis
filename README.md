# Quiz API Server

A RESTful API server for managing quizzes, allowing users to take quizzes, submit answers, and view their results.

## Project Structure

```
quiz-server/
├── Bootstrapper/           # ASP.NET Core application entry point
│   └── Api/                # Main API host project
├── Modules/                # Feature modules
│   └── Quiz/               # Quiz module 
│       └── Modules.Quiz/   # Quiz module implementation
│           ├── Data/       # Database migrations and seed data
│           ├── Domain/     # Domain models (Quiz, Question, Answer, etc.)
│           ├── Dto/        # Data transfer objects for API requests/responses
│           ├── Features/   # API endpoints organized by feature
│           └── Infrastructure/ # Repositories and data access
├── Shared/                 # Shared code and utilities
└── docker-compose.yml      # Docker configuration
```

## API Endpoints

### Quizzes

#### Get Quiz Questions

```
GET /api/quizzes/{quizId}/questions
```

Retrieves all questions and answer options for a specific quiz.

**Response:**
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "title": "Sample Quiz",
  "thumbnailUrl": "",
  "passingScore": 70,
  "questions": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "order": 1,
      "questionType": "multiple-choice",
      "text": "What is 2+2?",
      "explanation": "Basic addition",
      "answers": [
        {
          "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
          "text": "4",
          "isCorrect": true
        },
        {
          "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
          "text": "5",
          "isCorrect": false
        }
      ]
    }
  ]
}
```

### Quiz Attempts

#### Start a Quiz Attempt

```
POST /api/quizzes/{quizId}/attempts
```

Creates a new attempt for the specified quiz.

**Response:**
```json
{
  "attemptId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "quizId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "startedAt": "2025-05-10T09:30:00Z",
  "status": "in-progress"
}
```

#### Submit an Answer

```
POST /api/attempts/{attemptId}/answers
```

Submits an answer for a question in a quiz attempt.

**Request:**
```json
{
  "questionId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "selectedAnswerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

**Response:**
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "questionId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "selectedAnswerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "submittedAt": "2025-05-10T09:31:00Z",
  "isCorrect": true,
  "feedback": null
}
```

#### Complete a Quiz

```
POST /api/attempts/{attemptId}/complete
```

Marks a quiz attempt as completed and calculates the score.

**Response:**
```json
{
  "quizId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "status": "completed",
  "score": 80,
  "isPassed": true,
  "timeSpent": "00:10:30"
}
```

#### Get Quiz Results

```
GET /api/attempts/{attemptId}/results
```

Retrieves the results for a completed quiz attempt.

**Response:**
```json
{
  "quizId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "status": "completed",
  "score": 80,
  "isPassed": true,
  "timeSpent": "00:10:30",
  "totalQuestions": 10,
  "correctAnswers": 8,
  "incorrectAnswers": 2,
  "answerReviews": [
    {
      "questionId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "questionText": "What is 2+2?",
      "correctAnswerText": "4",
      "userSelectedAnswerText": "4",
      "isCorrect": true
    }
  ]
}
```

## Using Postman Collection

A Postman collection is included to help you test the API endpoints quickly.

### Importing the Collection

1. Download [Postman](https://www.postman.com/downloads/) if you don't have it installed
2. Open Postman
3. Click on "Import" button in the top left corner
4. Select "File" tab and choose the `Quiz.postman_collection.json` file
5. Click "Import" to add the collection to your workspace

### Running the Requests

The collection includes the following requests:

1. **List of questions** - Get all questions for a specific quiz
2. **Start quiz** - Create a new quiz attempt
3. **Submit answer** - Submit an answer for a question
4. **Complete quiz** - Mark an attempt as completed
5. **Get attempt result** - View the results of a completed attempt

### Notes on Usage

- When starting a new quiz, copy the `attemptId` from the response
- Update the attempt ID in the request URLs for subsequent requests
- The collection uses sample IDs that might need to be replaced with actual IDs from your database

## Database Schema

The application uses PostgreSQL with the following main entities:

- **Quiz**: Contains quiz metadata (title, passing score, etc.)
- **Question**: Questions belonging to a quiz
- **Answer**: Possible answers for each question
- **QuizAttempt**: Records of users attempting quizzes
- **UserAnswer**: Individual answers submitted by users

## Installation and Setup

### Prerequisites

- Docker and Docker Compose
- .NET 8 SDK (for development)

### Running with Docker

1. Clone the repository
   ```
   git clone <repository-url>
   cd quiz-server
   ```

2. Start the containers
   ```
   docker-compose up -d
   ```

3. Access the API at http://localhost:9000/swagger/index.html

### Development Setup

1. Install the .NET 8 SDK
2. Install PostgreSQL
3. Update the connection string in `appsettings.json` if needed
4. Run the application
   ```
   cd Bootstrapper/Api
   dotnet run
   ```

## Environment Configuration

The application uses the following environment variables:

- `ASPNETCORE_ENVIRONMENT`: Development, Staging, or Production
- `ASPNETCORE_HTTP_PORTS`: HTTP port (default: 8080)
- `ASPNETCORE_URLS`: URL configuration for the API

Database connection can be configured in `appsettings.json`:
