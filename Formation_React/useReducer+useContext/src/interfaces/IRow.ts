export default interface IRow {
	alias: string,
	cancelled: boolean,
	code: string,
	coordinates: {
		latitude: number,
		longitude: number,
	},
	country: {
		code: string,
		fullName: string,
		shortName: string
	},
	id: number,
	name: string,
	type: {
		id: number,
		code: string,
		label: string
	}
}