# messagePublisher
MessagePublish is a simple message broadcast tool that is the RabbitMQ message broker.

## Installation
```bash
git clone https://github.com/ump1love/messagePublisher.git
```

## Usage
# get executable from
cd MessagePublisher\bin\Release\net8.0\win-x64

MessagePublisher.exe --help

MessagePublisher.exe -s 1 -t Second -d 1024 -e my-exchange -f 23
MessagePublisher.exe -s 1 -d 1024 -e my-exchange -f 23
MessagePublisher.exe my-exchange -f 23
