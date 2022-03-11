# Shared Media Streamer
 Currently a proof of concept for learning video streaming consisting of an API and a website.
 Plans are to have an API that can read a video file and a website that calls on the API to get the video data streamed.
 
## C# ASP.NetCore MVC API
The API is written in C#. I have introduced the project to SOLID programming principles as well as carefully thought out architecuture.
It features clean code written with maintainability as priority. The API will read a certain amount of bytes from a media file at a certain offset
which is defined in the clients request. Once it has finished reading and retrieving the data, it will respond to the consumer.

#### Features:
###### 3-tiered Architecture:
- API layer - SDK, Dependency Service Container, Resolvers, Configuration file (app settings json file), Controllers
- Domain layer - Models, Interfaces, Repositories
- Media Data Processor layer - Media file reader

###### Code Quality:
- Good practise
- S.O.L.I.D design
- Software Design Principles 
