import {UserModel} from './userModel';
import {BugModel} from './bugModel';

export interface ProjectModel {
  name: string;
  bugCount: number;
  id: number;
  bugs: BugModel[];
  users: UserModel[];
}
