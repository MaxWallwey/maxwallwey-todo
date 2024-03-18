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
    <button onClick={() => onClick()} >
        <svg>
        </svg>
    </button>
);

const ButtonRemove = (onClick: Function) => (
    <button onClick={() => onClick()} >
        <svg>
        </svg>
    </button>
);

const ButtonAdd = () => (
    <button>
        <svg>
        </svg>
    </button>
);