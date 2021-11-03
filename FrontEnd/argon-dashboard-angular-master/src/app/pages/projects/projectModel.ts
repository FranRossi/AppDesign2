import {UserModel} from '../user-profile/userModel';

export interface ProjectModel {
  name: string;
  bugCount: number;
  id: number;
  bugs: [];
  users: [UserModel];
}
