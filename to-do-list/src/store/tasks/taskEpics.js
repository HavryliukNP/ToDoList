import { ofType } from 'redux-observable';
import { ajax } from 'rxjs/ajax';
import { mergeMap, map, catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import {
    fetchTasksStart,
    fetchTasksSuccess,
    fetchTasksFailure,
    addTaskStart,
    addTaskSuccess,
    addTaskFailure,
    updateTaskStatusStart,
    updateTaskStatusSuccess,
    updateTaskStatusFailure,
    deleteTaskStart,
    deleteTaskSuccess,
    deleteTaskFailure,
    fetchCategoriesStart,
    fetchCategoriesSuccess,
    fetchCategoriesFailure
} from './taskSlice';

const GRAPHQL_URL = 'http://localhost:5123/graphql';

const FETCH_TASKS_QUERY = `
  query GetTasks {
  taskQuery{
    tasks{
      id
      title
      description
      dueDate
      categoryId
      isCompleted
    }
  }
}
`;

const FETCH_CATEGORIES_QUERY = `
 query GetTasks {
  categoryQuery{
    categories{
      id
      name
    }
  }
}
`;

const ADD_TASK_MUTATION = `
  mutation($task: TaskInputType!) {
  taskMutation{
    addTask(task: $task) {
      id
      title
      description
      dueDate
      categoryId
      isCompleted
    }
  }
}
`;

const UPDATE_TASK_STATUS_MUTATION = `
  mutation($taskId: Int!, $isCompleted: Boolean!) {
  taskMutation{
     updateTaskStatus(taskId: $taskId, isCompleted: $isCompleted) {
      id
      title
      description
      dueDate
      categoryId
      isCompleted
    }
  }
}
`;

const DELETE_TASK_MUTATION = `
  mutation($taskId: Int!) {
  taskMutation{
    deleteTask(taskId: $taskId) {
      id
      title
      description
      dueDate
      categoryId
      isCompleted
    }
  }
}
`;

export const fetchCategoriesEpic = action$ => action$.pipe(
    ofType(fetchCategoriesStart.type),
    mergeMap(() =>
        ajax.post(GRAPHQL_URL, {
            query: FETCH_CATEGORIES_QUERY
        }, {
            'Content-Type': 'application/json'
        }).pipe(
            map(response => fetchCategoriesSuccess(response.response.data.categoryQuery.categories)),
            catchError(error => of(fetchCategoriesFailure(error.message)))
        )
    )
);

export const fetchTasksEpic = action$ => action$.pipe(
    ofType(fetchTasksStart.type),
    mergeMap(() =>
        ajax.post(GRAPHQL_URL, {
            query: FETCH_TASKS_QUERY
        }, {
            'Content-Type': 'application/json'
        }).pipe(
            map(response => fetchTasksSuccess(response.response.data.taskQuery.tasks)),
            catchError(error => of(fetchTasksFailure(error.message)))
        )
    )
);

export const addTaskEpic = action$ => action$.pipe(
    ofType(addTaskStart.type),
    mergeMap(action => {
        const { title, description, dueDate, categoryId, isCompleted } = action.payload;

        const task = {
            title,
            categoryId,
            isCompleted
        };

        if (description) {
            task.description = description;
        }

        if (dueDate) {
            const formattedDueDate = new Date(dueDate).toISOString();
            task.dueDate = formattedDueDate;
        }

        return ajax.post(GRAPHQL_URL, {
            query: ADD_TASK_MUTATION,
            variables: {
                task
            }
        }, {
            'Content-Type': 'application/json'
        }).pipe(
            mergeMap(response => [
                addTaskSuccess(response.response.data.taskMutation.addTask),
                fetchTasksStart() // Оновлення списку завдань після додавання нового
            ]),
            catchError(error => of(addTaskFailure(error.message)))
        );
    })
);


export const updateTaskStatusEpic = action$ => action$.pipe(
    ofType(updateTaskStatusStart.type),
    mergeMap(action =>
        ajax.post(GRAPHQL_URL, {
            query: UPDATE_TASK_STATUS_MUTATION,
            variables: {
                taskId: action.payload.taskId,
                isCompleted: action.payload.isCompleted
            }
        }, {
            'Content-Type': 'application/json'
        }).pipe(
            mergeMap(response => [
                updateTaskStatusSuccess(response.response.data.taskMutation.updateTaskStatus),
                fetchTasksStart() // Оновлення списку завдань після зміни статусу
            ]),
            catchError(error => of(updateTaskStatusFailure(error.message)))
        )
    )
);

export const deleteTaskEpic = action$ => action$.pipe(
    ofType(deleteTaskStart.type),
    mergeMap(action =>
        ajax.post(GRAPHQL_URL, {
            query: DELETE_TASK_MUTATION,
            variables: {
                taskId: action.payload
            }
        }, {
            'Content-Type': 'application/json'
        }).pipe(
            mergeMap(response => [
                deleteTaskSuccess(response.response.data.taskMutation.deleteTask),
                fetchTasksStart()
            ]),
            catchError(error => of(deleteTaskFailure(error.message)))
        )
    )
);
