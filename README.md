# JobsityChat
## Installation Setup 
### Option 1: Using Docker Compose 
 1. Clone project on your macheine: 
 ```
 git clone https://github.com/yeisonpx/JobsityChat.git
 ```
 2. Open a CMD Console or Terminar in Linux and change the current directory to the root folder of the project JobsityChat.
 3. Execute Docker-Compose Commands:

 ```
docker-compose build
docker-compose up
```
#### 4. Check local docker deployments URLs: 
- Web Chat: http://localhost:5001
- Chat Bot: http://localhost:5002
- RabbitMQ: http://localhost:15672
- SQL Server DB: http://localhost:5004

### Option 2: Manual Run with Visual Studio 
1. Firt at all, run RabbitMQ Docker Container: 
```
docker pull rabbitmq:3-management
docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```
2. Open project solution with Visual Studio:
4. Set JobSityChat.Bot and JobSityChat.Web as Startup Project.
5. Run the application. 

## Consideration Testing:
1. Project have .Net Identity so required to register and valide a user first. 
2. Login to see the chat. 
3. Ensure to have running RabbitMQ on port 5672 
4. Have installed .Net Core 3.1
5. Run the project with Visual Studio(Recommend 2019+)

Let me know if you have any questions :)
