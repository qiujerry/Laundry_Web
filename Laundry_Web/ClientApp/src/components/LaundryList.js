import React, { Component } from 'react';

export class LaundryList extends Component {
    static displayName = LaundryList.name;

    constructor(props) {
        super(props);
        this.state = { forecasts: [], loading: true };
    }

    componentDidMount() {
        this.populateWeatherData();

    }

    static calcRemainingTime(strData, mTimer, mStatus) {

        var date1 = new Date(strData);

        var hours = new Date().getHours(); //Current Hours
        var min = new Date().getMinutes(); //Current Minutes

        var ogMins = date1.getHours() * 60 + date1.getMinutes();
        var curMins = hours * 60 + min;

        var timePassed = curMins - ogMins;

        if (timePassed < 0) {
            curMins = curMins + 1440;

            timePassed = curMins - ogMins;
        }

        var timeRemaining = mTimer - timePassed;

        if (timeRemaining < 0 || mStatus.localeCompare("open") == 0) {
            timeRemaining = 0;
        }

        return timeRemaining;

    }

    static renderForecastsTable(laundryData) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Washer ID</th>
                        <th>Time Remaining</th>
                        <th>Status</th>

                    </tr>
                </thead>
                <tbody>
                    {laundryData.map(laundryData =>
                        <tr key={laundryData.machineNumber}>
                            <td>{laundryData.machineNumber}</td>
                            <td>{LaundryList.calcRemainingTime(laundryData.date, laundryData.timeSet, laundryData.available)}</td>
                            <td>{laundryData.available}</td>


                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : LaundryList.renderForecastsTable(this.state.forecasts);

        return (
            <div>
                <h1 id="tabelLabel" >Weather forecast</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }

    async populateWeatherData() {
        const response = await fetch('laundrylist');
        const data = await response.json();
        this.setState({ forecasts: data, loading: false });
    }

}