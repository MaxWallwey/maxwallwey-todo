type TodoType = {
    id: string;
    name: string;
    isComplete: boolean;
    createdAt: string;
};

export type FindManyTodosResponse = {
    data: TodoType[];
};

export type AddTodoRequest = {
    name: string;
};

export type CompleteTodoRequest = {
    id: string;
};

export type RemoveTodoRequest = {
    id: string;
};

export type ValidationError = {
    type: string;
    title: string;
    status: number;
    traceId: string;
    errors: {
        Name: Array<string>;
    };
};
