export class User {
  username: string;
  firstName: string;
  lastName: string;
  token?: string;
}


export class LoggedInUser {
  user: User;
  token: string;
}
