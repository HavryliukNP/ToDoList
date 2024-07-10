import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import {
    fetchTasksStart,
    addTaskStart,
    updateTaskStatusStart,
    deleteTaskStart,
    fetchCategoriesStart,
} from '../store/tasks/taskSlice';

function Main() {
    const dispatch = useDispatch();
    const { tasks, categories, loading, error } = useSelector(state => state.tasks);

    const [title, setTitle] = useState('');
    const [dueDate, setDueDate] = useState('');
    const [categoryId, setCategoryId] = useState('');
    const [description, setDescription] = useState('');
    const [showCategoryRequired, setShowCategoryRequired] = useState(false);

    useEffect(() => {
        dispatch(fetchTasksStart());
        dispatch(fetchCategoriesStart());
    }, [dispatch]);

    const handleSubmit = (e) => {
        e.preventDefault();

        if (!categoryId) {
            setShowCategoryRequired(true);
            return;
        }

        setShowCategoryRequired(false);

        const newTask = {
            title,
            categoryId: parseInt(categoryId),
            isCompleted: false,
        };
        if (description) {
            newTask.description = description;
        }
        if (dueDate) {
            newTask.dueDate = dueDate;
        }
        dispatch(addTaskStart(newTask));
        setTitle('');
        setDueDate('');
        setDescription('');
        setCategoryId('');
    };


    if (loading) {
        return <div>Loading...</div>;
    }

    if (error) {
        return <div>Error: {error}</div>;
    }

    return (
        <div>
            <h1 className="mb-3 text-center">Створення задачі</h1>
            <form id="createTaskForm" onSubmit={handleSubmit}>
                <div className="mb-3">
                    <label className="form-label" htmlFor="title">Назва задачі</label>
                    <input
                        id="title"
                        name="title"
                        type="text"
                        className="form-control"
                        value={title}
                        onChange={(e) => setTitle(e.target.value)}
                        required
                    />
                </div>
                <div className="mb-3">
                    <label className="form-label" htmlFor="dueDate">Дата виконання (необов’язково)</label>
                    <input
                        id="dueDate"
                        name="dueDate"
                        type="date"
                        className="form-control"
                        value={dueDate}
                        onChange={(e) => setDueDate(e.target.value)}
                    />
                </div>
                <div className="mb-3">
                    <label className="form-label" htmlFor="CategoryId">Категорія задачі</label>
                    <select
                        id="CategoryId"
                        name="CategoryId"
                        className={`form-select ${showCategoryRequired && !categoryId ? 'form-select-required' : ''}`}
                        value={categoryId}
                        onChange={(e) => {
                            setCategoryId(e.target.value);
                            setShowCategoryRequired(false);
                        }}
                        required
                    >
                        <option value="">Виберіть категорію</option>
                        {categories.map(category => (
                            <option key={category.id} value={category.id}>{category.name}</option>
                        ))}
                    </select>
                </div>
                <div className="mb-3">
                    <label className="form-label" htmlFor="description">Опис задачі</label>
                    <textarea
                        id="description"
                        name="description"
                        className="form-control"
                        rows="3"
                        value={description}
                        onChange={(e) => setDescription(e.target.value)}
                    ></textarea>
                </div>
                <button id="createTask" type="submit" className="btn btn-primary">Створити</button>
            </form>

            <h2 className="text-center mt-5">Всі задачі</h2>
            <table className="table">
                <thead>
                <tr>
                    <th>Назва</th>
                    <th>Опис</th>
                    <th>Дата виконання</th>
                    <th>Категорія</th>
                    <th>Виконано</th>
                    <th></th>
                </tr>
                </thead>
                <tbody>
                {tasks && tasks.length > 0 ? (
                    tasks.map(task => (
                        <tr key={task.id} className={task.isCompleted ? "completed-task" : ""}>
                            <td>{task.title}</td>
                            <td>{task.description}</td>
                            <td>{task.dueDate ? new Date(task.dueDate).toLocaleDateString() : ""}</td>
                            <td>{categories.find(category => category.id === task.categoryId)?.name}</td>
                            <td>
                                <input
                                    type="checkbox"
                                    name="isCompleted"
                                    checked={task.isCompleted}
                                    onChange={() => dispatch(updateTaskStatusStart({ taskId: task.id, isCompleted: !task.isCompleted }))} // зміна статусу
                                />
                            </td>
                            <td>
                                <button
                                    className="btn btn-link p-0"
                                    onClick={() => {
                                        dispatch(deleteTaskStart(task.id));
                                    }}
                                >
                                    <i className="fa-solid fa-trash deleteTask"></i>
                                </button>
                            </td>
                        </tr>
                    ))
                ) : (
                    <tr>
                        <td colSpan="6" className="text-center">Немає доступних задач</td>
                    </tr>
                )}
                </tbody>
            </table>
        </div>
    );
}

export default Main;
