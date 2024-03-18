import { FormEvent, useContext, useState } from "react";
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
            <div>
                <h1>Todo List</h1>
                <form onSubmit={returnNameCallback}>
                    <input
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
                    <div>
                        <p>{apiResponseMessage}</p>
                    </div>
                )}
            </div>
        </>
    );
};