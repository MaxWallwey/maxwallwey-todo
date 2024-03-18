import {
    AddTodoRequest,
    CompleteTodoRequest,
    FindManyTodosResponse,
    RemoveTodoRequest,
} from './apiTypes';

const get = async (url: string, token: string) => {
    const response = await fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${token}`,
        },
    });
    return await response.json();
};

async function post<TRequestData>(
    url: string,
    token: string,
    data?: TRequestData
) {
    const response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json; charset=UTF-8',
            Accept: 'application/json',
            Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(data),
    });
    return await response;
}

const apiBaseAddress = process.env.REACT_APP_API_BASE_ADDRESS;

export const findMany = async (
    token: string
): Promise<FindManyTodosResponse> => {
    return await get(`${apiBaseAddress}/todo.findMany`, token);
};

export const remove = async (request: RemoveTodoRequest, token: string) => {
    return await post(`${apiBaseAddress}/todo.remove?id=${request.id}`, token);
};

export const complete = async (request: CompleteTodoRequest, token: string) => {
    return await post(`${apiBaseAddress}/todo.complete?id=${request.id}`, token);
};

export const add = async (request: AddTodoRequest, token: string) => {
    return await post(`${apiBaseAddress}/todo.add`, token, request);
};
