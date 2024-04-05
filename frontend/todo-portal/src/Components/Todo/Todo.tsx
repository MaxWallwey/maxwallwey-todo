import { Buttons, TodoButton } from "../TodoButton";
import React from "react";

type TodoProp = {
  id: string;
  name: string;
  isComplete: boolean;
  removeTodo: () => void;
  completeTodo: () => void;
};

export const Todo: React.FC<TodoProp> = ({
  id,
  name,
  isComplete,
  removeTodo,
  completeTodo,
}) => (
  <>
    <div className="flex mb-4 items-center" id={id}>
      <p className={`w-full`}>
        <span className={isComplete ? "font-style: italic" : ""}>{name}</span>
      </p>
      {!isComplete && (
        <TodoButton variant={Buttons.complete} onClick={completeTodo} />
      )}
      {isComplete && (
        <button className="flex-no-shrink py-1 px-2 border-2 rounded text-xl  bg-green border-green hover:brightness-110">
          ✔️
        </button>
      )}
      &nbsp;&nbsp;
      <TodoButton variant={Buttons.remove} onClick={removeTodo} />
    </div>
  </>
);
