import React, { FormEvent, useContext, useState } from "react";
import { ApiResponseContext } from "../../Context";
import { Buttons, TodoButton } from "../TodoButton";

export const TodoListHeader: React.FC<{ callback: Function }> = ({
                                                                     callback,
                                                                 }) => {
    const [name, setName] = useState("");
    const { apiResponseMessage, apiResponseCode } =
        useContext(ApiResponseContext);

    const returnNameCallback = (event: FormEvent) => {
        event.preventDefault();
        callback(name);
        setName("");
    };

    return (
        <>
            <div className="mb-4">
                <h1 className="font-bold text-5xl">Todo List</h1>
                <form className="flex mt-5 mb-4" onSubmit={returnNameCallback}>
                    <input
                        className={`shadow appearance-none border rounded w-full py-2 px-3 mr-2 ${
                            apiResponseCode === 400 && "border-red/80 focus:outline-red"
                        }`}
                        placeholder="Add Todo"
                        value={name}
                        required
                        onChange={(event) => {
                            setName(event.target.value);
                        }}
                    />
                    <TodoButton variant={Buttons.add} />
                </form>
                {apiResponseMessage && apiResponseCode !== 200 && (
                    <div className={"p-3 bg-red/20 mb-6 rounded"}>
                        <p className={"text-red font-bold"}>{apiResponseMessage}</p>
                    </div>
                )}
            </div>
        </>
    );
};