import { render, screen } from "@testing-library/react";
import { TodoButton, Buttons } from "./TodoButton";

describe("GIVEN the TodoButton", () => {
    describe('WHEN it is of type "remove"', () => {
        it("THEN it should have a red background", () => {
            render(<TodoButton variant={Buttons.remove} />);
            expect(screen.getByRole("button")).toHaveClass("bg-red");
        });
    });

    describe('WHEN it is of type "complete"', () => {
        it("THEN it should have a green background", () => {
            render(<TodoButton variant={Buttons.complete} />);
            expect(screen.getByRole("button")).toHaveClass("bg-green");
        });
    });

    describe('WHEN it is of type "Add"', () => {
        it("THEN it should have a blue background", () => {
            render(<TodoButton variant={Buttons.add} />);
            expect(screen.getByRole("button")).toHaveClass("bg-blue");
        });
    });
});