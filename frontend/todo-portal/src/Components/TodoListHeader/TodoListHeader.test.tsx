import { render, screen } from "@testing-library/react";
import { TodoListHeader } from "./TodoListHeader";

describe("GIVEN a TodoListHeader", () => {
    describe("WHEN it renders", () => {
        it("THEN it should have specified title", () => {
            render(
                <TodoListHeader
                    callback={() => {
                        return;
                    }}
                />
            );
            expect(screen.getByText("Todo List")).toBeInTheDocument();
        });
    });
});