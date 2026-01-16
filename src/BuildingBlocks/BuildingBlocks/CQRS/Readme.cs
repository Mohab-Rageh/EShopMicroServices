/*
1.Command Handler Type => IRequestHandler<CommandQueryType,ResultType>  //IRequestHandler is from MediatR library
2.Command Query Type => CommandQueryType : IRequest<ResultType>  //IRequest is from MediatR library and the result is the output from this request
3.Result Type => ResultType  //This is the type of the result returned by the command handler
*/