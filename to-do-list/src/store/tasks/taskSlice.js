import { createSlice } from '@reduxjs/toolkit';

const initialState = {
    tasks: [],
    categories: [],
    loading: false,
    error: null,
};

const taskSlice = createSlice({
    name: 'tasks',
    initialState,
    reducers: {
        fetchCategoriesStart: (state) => {
            state.loading = true;
            state.error = null;
        },
        fetchCategoriesSuccess: (state, action) => {
            state.loading = false;
            state.categories = action.payload;
        },
        fetchCategoriesFailure: (state, action) => {
            state.loading = false;
            state.error = action.payload;
        },
        fetchTasksStart: (state) => {
            state.loading = true;
            state.error = null;
        },
        fetchTasksSuccess: (state, action) => {
            state.loading = false;
            state.tasks = action.payload;
        },
        fetchTasksFailure: (state, action) => {
            state.loading = false;
            state.error = action.payload;
        },
        addTaskStart: (state) => {
            state.loading = true;
            state.error = null;
        },
        addTaskSuccess: (state, action) => {
            state.loading = false;
            state.tasks.push(action.payload); // Додаємо нове завдання в кінець масиву
        },
        addTaskFailure: (state, action) => {
            state.loading = false;
            state.error = action.payload;
        },
        updateTaskStatusStart: (state) => {
            state.loading = true;
            state.error = null;
        },
        updateTaskStatusSuccess: (state, action) => {
            state.loading = false;
            state.tasks = state.tasks.map(task =>
                task.id === action.payload.id ? { ...task, ...action.payload } : task
            );
        },
        updateTaskStatusFailure: (state, action) => {
            state.loading = false;
            state.error = action.payload;
        },
        deleteTaskStart: (state) => {
            state.loading = true;
            state.error = null;
        },
        deleteTaskSuccess: (state, action) => {
            state.loading = false;
            state.tasks = state.tasks.filter(task => task.id !== action.payload);
        },
        deleteTaskFailure: (state, action) => {
            state.loading = false;
            state.error = action.payload;
        },
    },
});

export const {
    fetchCategoriesStart,
    fetchCategoriesSuccess,
    fetchCategoriesFailure,
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
} = taskSlice.actions;

export default taskSlice.reducer;