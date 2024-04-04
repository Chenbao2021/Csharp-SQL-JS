import { MfFormField, MfTextField } from "@mf/react-mui-controls"
import { useGridDataContext } from "../reducer/GridContext";

function Details() {
	const context = useGridDataContext()
	const { gridData } = context!;
	const { selectedData } = gridData;

	return (
		<div className='cDetails'>
			<MfFormField label="Name">
				<MfTextField id="sDetailName" value={selectedData?.name} aria-readonly />
			</MfFormField>
			<MfFormField label="Alias" >
				<MfTextField id="sDetailAlias" value={selectedData?.alias} aria-readonly />
			</MfFormField>
			<MfFormField label="Type Label" >
				<MfTextField id="sDetailTypeLabel" value={selectedData?.type?.label} aria-readonly />
			</MfFormField>
			<MfFormField label="Code">
				<MfTextField id="sDetailCode" value={selectedData?.code} aria-readonly />
			</MfFormField>
			<MfFormField label="Latitude" >
				<MfTextField id="sDetailLatitude" value={selectedData?.coordinates?.latitude || ""} aria-readonly />
			</MfFormField>
			<MfFormField label="Longitude">
				<MfTextField id="sDetailLongitude" value={selectedData?.coordinates?.longitude || ""} aria-readonly />
			</MfFormField>
			<MfFormField label="Country">
				<MfTextField id="sDetailCountry" value={selectedData?.country?.fullName} aria-readonly />
			</MfFormField>
		</div>
	)
}

export default Details