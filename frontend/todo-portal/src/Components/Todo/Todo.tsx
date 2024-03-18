import {Buttons, TodoButton} from "../TodoButton";

type TodoProp = {
    id: string;
    name: string;
    isComplete: boolean;
    createdAt: string;
    removeTodo: () => void;
    completeTodo: () => void;
}

export const Todo: React.FC<TodoProp> = ({
    id,
    name,
    isComplete,
    createdAt,
    removeTodo,
    completeTodo,
}) => (
    <>
        <div>
            <p>
                <span>{name}</span>
            </p>

            {!isComplete && (
                <TodoButton variant={Buttons.complete} onClick={completeTodo} />
            )}

            <TodoButton variant={Buttons.remove} onClick={removeTodo} />
        </div>
    </>
);