import {UserModel} from './userModel';
import {BugModel} from './bugModel';
import {AssignmentModel} from './assignmentModel';

export interface ProjectModel {
  name: string;
  bugCount: number;
  id: number;
  bugs: BugModel[];
  users: UserModel[];
  assignments: AssignmentModel[];
  duration: number;
  cost: number;
}
