import { produce } from "immer";
import IRow from "../interfaces/IRow";
import { useReducer, createContext } from "react";

export interface IState {
	selectedData?: IRow,
	data?: IRow[],
	number?: number,
}

export const initialState: IState = {
	selectedData: undefined,
	data: [],
	number: 2554,
}

export function getInitialState(module: any): IState {
	return {
		...initialState,
	}
}

export enum ActionEnum {
	setSelectedData = "setSelectedData",
	setData = "setData",
	test = "Test"
}

export type Action =
	{ type: ActionEnum.setSelectedData, value?: IRow }
	| { type: ActionEnum.setData, value?: IRow[] }
	| { type: ActionEnum.test }

export const gridReducer = produce((draft: IState, action: Action) => {
	switch (action.type) {
		case ActionEnum.setSelectedData:
			draft.selectedData = action.value
			break;
		case ActionEnum.setData:
			draft.data = action.value
			break;
		case ActionEnum.test:
			draft.number = 8888;
			break;
	}
	return draft;
});
