# linux-dns  

This is a DNS query test app written in .net core v3.1 

## Usage:

#### __LinuxDnsQuery__ (no arguments) 
Performs a single DNS query for yahoo.com  

#### __LinuxDnsQuery [hostname]__ 
Performs a single DNS query using the hostname of your choice. 

#### __LinuxDnsQuery [hostname <iterations>]__ 
Performs DNS ~n DNS queries for the chosen hostname. 

#### __LinuxDnsQuery [hostname <iterations> [delay]]__ 
Performs DNS ~n DNS queries for the chosen hostname but introduces a configurable delay between queries.  
  
    
    
## Docker Desktop Instructions

Note: You'll want to run two windows terminal instances. One for compiling and copying and one for running the docker instance. If not using Docker Desktop, deploy how you normally deploy.

- From withing the VS project folder compile for the desired linux variant: 

    Example: dotnet publish -c release -r ubuntu.20.04-x64 

- Copy the files

    docker cp . containerid:/appfoldername  
    Example: __docker cp . ddeb5d322804:/LinuxDnsQuery__

- Start a linux instance in docker in a separate terminal instance:

     docker run -it ubuntu

- CD into the app folder and set execute permissions for the executable:

    __chmod 777 ./LinuxDnsQuery__

- Execute the app. Examples:

    __LinuxDnsQuery__ <-- query yahoo.com  
    __LinuxDnsQuery yahoo.com__ <-- query yahoo.com  
    __LinuxDnsQuery yahoo.com 100__ <-- query yahoo.com 100 times with no delay between requests.  
    __LinuxDnsQuery yahoo.com 1000 100__ <-- query yahoo.com 1000 times while introducing a 100ms delay between queries.

Note: Docker Desktop on Windows will use the underlying Windows DNS cache!