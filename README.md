# SerilogLIibraries

This creates re-usable Serilog libraries for both ASP.Core and Asp.Net

The shared Serilog Standard library is a .net Standard 2.0 library that referenced by both Asp.Core and Asp.Net 4.6.1 serilog libraries
The Serilog Asp.Core library has Asp.Core specific middlewares while .NetFramework library has .net framework specific middlewares.   The reusable middlewares can be injected into respective 
web start up projects and reused with simple middleware injection.

The following are the diagram depict the relationship amount libraries. 
                     
                     SerilogLibrary (.Net Standard 2.0 Library)
                                /|\
                                 |
                    _____________|_________________
                   |                              |
                   |                              |
             SerilogLibrary.NetCore           SerilogLibraryFramework
                   |                              |
                   |                              |
                   |                              |
                   |                              |
Serilog provides nice structured logging output.  It can be channeled into any other logging or monitoring tools, such as ELK (elasticSearch, Logstash and Kibana)

I setup ELK in dockers and was able to see the logs ingested into ELK. 
