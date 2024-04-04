import React, { useReducer } from 'react'
import { getInitialState, gridReducer } from './gridReducer'
import GridDataContext from './GridContext';

export function GridProvider({ children }: { children: React.ReactNode }) {
	const [gridData, dispatch] = useReducer(gridReducer, undefined, getInitialState);
	return (
		<GridDataContext.Provider value={{ gridData, dispatch }}>
			{children}
		</GridDataContext.Provider>
	)
}
