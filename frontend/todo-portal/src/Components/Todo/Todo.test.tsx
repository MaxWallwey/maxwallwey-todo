import { cleanup, render, screen } from "@testing-library/react";
import { Todo } from "./Todo";

describe("GIVEN a Todo component", () => {
    beforeEach(() => {
        cleanup();
    });

    describe("WHEN isComplete is set to true", () => {
        it("THEN it should have one button", async () => {
            render(
                <Todo
                    id={Math.random().toString()}
                    name={"test todo"}
                    isComplete={true}
                    removeTodo={() => {
                        return;
                    }}
                    completeTodo={() => {
                        return;
                    }}
                />
            );
            expect(await screen.getAllByRole("button").length).toBe(1);
        });
        it("THEN that button be red", async () => {
            render(
                <Todo
                    id={Math.random().toString()}
                    name={"test todo"}
                    isComplete={true}
                    removeTodo={() => {
                        return;
                    }}
                    completeTodo={() => {
                        return;
                    }}
                />
            );
            expect(screen.getByRole("button")).toHaveClass("bg-red");
        });
    });

    describe("WHEN isComplete is false", () => {
        it("THEN it should have two buttons", async () => {
            render(
                <Todo
                    id={Math.random().toString()}
                    name={"test todo"}
                    isComplete={false}
                    removeTodo={() => {
                        return;
                    }}
                    completeTodo={() => {
                        return;
                    }}
                />
            );
            expect(await screen.getAllByRole("button").length).toBe(2);
        });
    });
});