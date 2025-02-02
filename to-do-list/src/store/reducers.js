import { combineReducers } from 'redux';
import tasksReducer from './tasks/taskSlice';

const rootReducer = combineReducers({
    tasks: tasksReducer,
});

export default rootReducer;
