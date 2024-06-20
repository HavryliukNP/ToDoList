import React, { useState } from 'react';

function Main({ categories, tasks, addTask, updateTaskStatus, deleteTask }) {
    const uncompletedTasks = tasks.filter(task => !task.isCompleted).sort((a, b) => b.id - a.id);
    const completedTasks = tasks.filter(task => task.isCompleted);

    const [title, setTitle] = useState('');
    const [dueDate, setDueDate] = useState('');
    const [categoryId, setCategoryId] = useState('');
    const [description, setDescription] = useState('');

    const handleSubmit = (e) => {
        e.preventDefault();

        const newTask = {
            id: tasks.length + 1,
            title,
            description,
            dueDate,
            categoryId: parseInt(categoryId),
            isCompleted: false,
        };

        addTask(newTask);

        setTitle('');
        setDueDate('');
        setDescription('');
        setCategoryId('');
    };

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
                        className="form-control"
                        value={categoryId}
                        onChange={(e) => setCategoryId(e.target.value)}
                    >
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
                {uncompletedTasks.map(task => (
                    <tr key={task.id} className={task.isCompleted ? "completed-task" : ""}>
                        {/* Render task details */}
                        <td>{task.title}</td>
                        <td>{task.description}</td>
                        <td>{task.dueDate ? new Date(task.dueDate).toLocaleDateString() : ""}</td>
                        <td>{categories.find(category => category.id === task.categoryId)?.name}</td>
                        <td>
                            <input
                                type="checkbox"
                                name="isCompleted"
                                value="true"
                                defaultChecked={task.isCompleted}
                                onChange={(e) => {
                                    updateTaskStatus(task.id, e.target.checked);
                                }}
                            />
                        </td>
                        <td>
                            <button
                                className="btn btn-link p-0"
                                onClick={() => {
                                    deleteTask(task.id);
                                }}
                            >
                                <i className="fa-solid fa-trash deleteTask"></i>
                            </button>
                        </td>
                    </tr>
                ))}
                {completedTasks.map(task => (
                    <tr key={task.id} className={task.isCompleted ? "completed-task" : ""}>
                        {/* Render task details */}
                        <td>{task.title}</td>
                        <td>{task.description}</td>
                        <td>{task.dueDate ? new Date(task.dueDate).toLocaleDateString() : ""}</td>
                        <td>{categories.find(category => category.id === task.categoryId)?.name}</td>
                        <td>
                            <input
                                type="checkbox"
                                name="isCompleted"
                                value="true"
                                defaultChecked={task.isCompleted}
                                onChange={(e) => {
                                    updateTaskStatus(task.id, e.target.checked);
                                }}
                            />
                        </td>
                        <td>
                            <button
                                className="btn btn-link p-0"
                                onClick={() => {
                                    deleteTask(task.id);
                                }}
                            >
                                <i className="fa-solid fa-trash deleteTask"></i>
                            </button>
                        </td>
                    </tr>
                ))}
                </tbody>
            </table>
        </div>
    );
}

export default Main;
