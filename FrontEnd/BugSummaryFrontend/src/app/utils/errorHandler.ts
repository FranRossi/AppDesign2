export class ErrorHandler {

  public static onHandleError(error) {
    let message: string;
    if (error.statusCode === 0 || (error != null && error.error instanceof ProgressEvent)) {
      message = 'Cannot connect to WebApi';
    } else {
      message = error.error;
    }
    return message;
  }
  
}
