# Code Challenge - Chat Room

This WebAPI repository was built as a code challenge to Power Diary. <br/>
It's built with .NET6 as a DDD application, using Entity Framework Core  with InMemory Database and UnitOfWork. I also implemented the Mapper functionality and all the requests parameters are being validated by FluentValidation. <br/>
The tests were written with xUnit with Moq and TestFixture.
<br/>
The presentation contains a Swagger Documentation and a Console Application for easier demonstration. <br/>
All SOLID principles were respected.


## Running the Samples From the Command Line
Clone this repository:
```
    $ git clone https://github.com/dschaly/chat-challenge.git

```

# API endpoints

These endpoints allow you to:
* Enter the chat room
* Leave the chat room
* Post a Comment
* Send a high-five to another user
* List all actions by hour
* List all actions by minute
* List all users

## GET
`list-all-actions` ChatActions/get-all-actions <br/>
`list-all-users` ChatActions/get-all-user <br/>
`get-history-by-minute` ChatActions/get-history-by-minute <br/>
`get-history-by-hour` ChatActions/get-history-by-hour <br/>

## POST
`enter-the-room` [ChatActions/enter-the-room](#post-1billingstart-trialjson) <br/>
`leave-the-room` [ChatActions/leave-the-room](#post-1billingcancel-trialjson) <br/>
`comment` [ChatActions/comment](#post-1billingstart-or-update-subscriptionjson) <br/>
`high-five` [ChatActions/high-five](#post-1billingcancel-subscriptionjson) <br/>

## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.
