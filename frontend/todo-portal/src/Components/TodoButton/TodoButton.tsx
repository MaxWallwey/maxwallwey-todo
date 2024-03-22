import React from "react";

export const enum Buttons {
    complete,
    remove,
    add
}

export const TodoButton: React.FC<{
    variant: Buttons;
    onClick?: Function;
}> = ({ variant, onClick }) => {
    switch (variant) {
        case Buttons.remove:
            return ButtonRemove(onClick!);
        case Buttons.complete:
            return ButtonComplete(onClick!);
        case Buttons.add:
            return ButtonAdd();
        default:
            throw new DOMException("Button type doesn't exist");
    }
};

const ButtonComplete = (onClick: Function) => (
    <button
        className="flex-no-shrink py-1 px-2 border-2 rounded text-xl  bg-gray-dark border-gray-dark hover:brightness-110"        
        onClick={() => onClick()} >
        <span aria-label="Light mode" role="img" className={""}>✔️</span>
    </button>
);

const ButtonRemove = (onClick: Function) => (
    <button
        className="flex-no-shrink py-1 px-2 border-2 rounded text-xl  bg-gray-dark border-gray-dark hover:brightness-110"        
        onClick={() => onClick()}>
        <span aria-label="Light mode" role="img">➖</span>
    </button>
);

const ButtonAdd = () => (
    <button className="flex-no-shrink py-1 px-2 border-2 rounded text-xl  bg-gray-dark border-gray-dark hover:brightness-110">
        <span aria-label="Light mode" role="img">➕</span>
    </button>
);