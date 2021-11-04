import {UserModel} from '../user-profile/userModel';
import {BugModel} from './project/editProject/bugModel';

export interface ProjectModel {
  name: string;
  bugCount: number;
  id: number;
  bugs: [BugModel];
  users: [UserModel];
}
