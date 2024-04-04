import React from "react"
import { Action, IState } from "./gridReducer";

const GridDataContext = React.createContext<{ gridData: IState, dispatch: React.Dispatch<Action> } | null>(null)

export function useGridDataContext() {
	return React.useContext(GridDataContext);
}

export default GridDataContext;