import {UserModel} from './userModel';
import {BugModel} from './bugModel';

export interface ProjectListModel {
  name: string;
  bugCount: number;
  id: number;
}
