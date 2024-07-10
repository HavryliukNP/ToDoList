import { combineEpics } from 'redux-observable';
import {
    fetchTasksEpic,
    addTaskEpic,
    updateTaskStatusEpic,
    deleteTaskEpic,
    fetchCategoriesEpic,
} from './tasks/taskEpics';

export const rootEpic = combineEpics(
    fetchTasksEpic,
    addTaskEpic,
    updateTaskStatusEpic,
    deleteTaskEpic,
    fetchCategoriesEpic
);
