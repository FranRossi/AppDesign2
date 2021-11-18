export class ErrorHandler {


  public static onHandleError(error) {
    let message: string;
    if (error.statusCode === 0) {
      message = 'Cannot connect to WebApi';
    } else {
      message = error.error;
    }
    return message;
  }
}
