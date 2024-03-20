import { useAuth } from "react-oidc-context";
import { Todo } from "../Todo";
import { TodoListHeader } from "../TodoListHeader";
import React, { useContext, useEffect, useState } from "react";
import {
    add,
    AddTodoRequest,
    complete,
    CompleteTodoRequest,
    findMany,
    FindManyTodosResponse,
    remove,
    RemoveTodoRequest,
    ValidationError,
} from "../../api";
import { ApiResponseContext } from "../../Context";

export const TodoList: React.FC = () => {
    const [todos, setTodos] = useState<FindManyTodosResponse>();
    const { apiResponseMessage, apiResponseCode, setApiResponse } =
        useContext(ApiResponseContext);
    const auth = useAuth();
    useEffect(() => {
        findManyTodos();
    }, []);

    useEffect(() => {
        let timer: any;
        if (apiResponseCode === 200) {
            timer = setTimeout(() => {
                setApiResponse("", 0);
            }, 2000);
        }
        return () => {
            clearTimeout(timer);
        };
    }, [apiResponseMessage, apiResponseCode, setApiResponse]);

    const findManyTodos = async () => {
        await findMany(auth.user!.access_token).then((response) => {
            setTodos(response);
        });
    };

    const removeTodo = async (id: string, token: string) => {
        const request: RemoveTodoRequest = { id };
        await remove(request, token).then(async (response) => {
            if (response.ok) {
                await findManyTodos();
                setApiResponse("Todo removed", response.status);
            }
            else {
                setApiResponse(`${request.id} | ${response.url}`, response.status);
            }
        });
    };

    const completeTodo = async (id: string, token: string) => {
        const request: CompleteTodoRequest = { id };
        await complete(request, token).then(async (response) => {
            if (response.ok) {
                await findManyTodos();
                setApiResponse("Todo marked as complete", response.status);
            }
        });
    };

    const addTodo = async (name: string, token: string) => {
        const request: AddTodoRequest = { name };
        await add(request, token).then(async (response) => {
            if (response.ok) {
                await findManyTodos();
                setApiResponse(`${name} added to todo list`, response.status);
            } else {
                const errors: ValidationError = await response.json();
                setApiResponse(`${errors.errors.Name[0]}`, response.status);
            }
        });
    };

    const getTodoListHeaderNameInputValue = (name: string) => {
        addTodo(name, auth.user!.access_token);
    };

    return (
        <div className="bg-white rounded shadow p-6 m-4 w-full lg:w-3/4 md:w-1/2 lg:max-w-lg relative">
            <TodoListHeader callback={getTodoListHeaderNameInputValue} />
            <div>
                {apiResponseMessage && apiResponseCode === 200 && (
                    <div className={"p-3 bg-green/20 mb-6 rounded"}>
                        <p className={"text-green font-bold"}>{apiResponseMessage}</p>
                    </div>
                )}
                {todos != null ? (
                    todos.data?.map(
                        (todo: { id: string; name: string; isComplete: boolean; createdAt: string; }) => (
                            <Todo
                                key={todo.id}
                                id={todo.id}
                                name={todo.name}
                                isComplete={todo.isComplete}
                                removeTodo={async () =>
                                    removeTodo(todo.id, auth.user!.access_token)
                                }
                                completeTodo={async () =>
                                    completeTodo(todo.id, auth.user!.access_token)
                                }
                            />
                        )
                    )
                ) : (
                    <div>
                        <p className={"text-red"}>Todo list empty</p>
                    </div>
                )}
            </div>
        </div>
    );
};