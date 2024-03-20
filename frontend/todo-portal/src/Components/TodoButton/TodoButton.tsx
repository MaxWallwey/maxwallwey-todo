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
        className="flex-no-shrink p-2 ml-4 mr-2 border-2 rounded bg-green border-green hover:brightness-110"
        onClick={() => onClick()} >
        <svg
            className="w-5 h-5 fill-white"
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 512 512"
        >
            <path
                d="m493.3 128-22.6 22.6-256 256-22.7 22.7-22.6-22.6-128-128L18.7 256 64 210.7l22.6 22.6L192 338.7l233.4-233.3L448 82.7l45.3 45.3z"/>
        </svg>
    </button>
);

const ButtonRemove = (onClick: Function) => (
    <button
        className="flex-no-shrink p-2 border-2 rounded bg-red border-red hover:brightness-110"
        onClick={() => onClick()}>
        <svg
            className="w-5 h-5 fill-white"
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 320 512"
        >
            <path
                d="m294.6 166.6 22.7-22.6L272 98.7l-22.6 22.6-89.4 89.4-89.4-89.3L48 98.7 2.7 144l22.6 22.6 89.4 89.4-89.3 89.4L2.7 368 48 413.3l22.6-22.6 89.4-89.4 89.4 89.4 22.6 22.6 45.3-45.3-22.6-22.6-89.4-89.4 89.4-89.4z"/>
        </svg>
    </button>
);

const ButtonAdd = () => (
    <button className="flex-no-shrink py-1 px-2 border-2 rounded text-xl  bg-blue border-blue hover:brightness-110">
        <svg
            className="w-5 fill-white"
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 448 512"
        >
            <path d="M256 80V48h-64v176H16v64h176v176h64V288h176v-64H256V80z"/>
        </svg>
    </button>
);