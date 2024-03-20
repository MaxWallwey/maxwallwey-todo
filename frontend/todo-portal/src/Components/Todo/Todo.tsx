import {Buttons, TodoButton} from "../TodoButton";
import React from "react";

type TodoProp = {
    id: string;
    name: string;
    isComplete: boolean;
    removeTodo: () => void;
    completeTodo: () => void;
}

export const Todo: React.FC<TodoProp> = ({
    id,
    name,
    isComplete,
    removeTodo,
    completeTodo,
}) => (
    <>
        <div className="flex mb-4 items-center" id={id}>
            <p className={`w-full ${isComplete ? "text-green" : ""}`}>
                <span className={isComplete ? "bg-green/20" : ""}>{name}</span>
            </p>

            {!isComplete && (
                <TodoButton variant={Buttons.complete} onClick={completeTodo} />
            )}

            <TodoButton variant={Buttons.remove} onClick={removeTodo} />
        </div>
    </>
);