import "./Dashboard.css";
import React, { useEffect } from "react";
import MyBarChart from "./MyBarChart/MyBarChart";
import MyAreaLine from "./MyAreaLine/MyAreaLine";
import axios from "axios";

const data = [
  {
    name: '1',
    value: 2
  },
  {
    name: '2',
    value: 24
  },
  {
    name: '3',
    value: 40
  },
  {
    name: '4',
    value: 100
  },
  {
    name: '5',
    value: 129
  },
  {
    name: '6',
    value: 137
  },
  {
    name: '7',
    value: 140
  },
  {
    name: '8',
    value: 144
  },
  {
    name: '9',
    value: 150
  },
  {
    name: '10',
    value: 157
  },
  {
    name: '11',
    value: 160
  },
  {
    name: '12',
    value: 163
  },
  {
    name: '13',
    value: 168
  },
  {
    name: '14',
    value: 171
  },
  {
    name: '15',
    value: 178
  },
  {
    name: '16',
    value: 183
  },
];

const Dashboard = (props) => {
  debugger;
  console.log(props);
  useEffect(() => {
    axios.get("http://localhost:5177/api/Dashboard").then(responce => {
      props.setDataForDashboard(responce.data.day, responce.data.month, responce.data.year);
  })
  }, []);

  return (
    <div className="flexContainerForDashboard">
      <div className='containerForBarChart'>
        <div className='BarChartAndHeader'>
          <h2>Количество посещений за сегодня</h2>
          <MyBarChart data={props.visitToDay} />
        </div>
      </div>
      <div className='containerForBarChart'>
        <div className='BarChartAndHeader'>
          <h2>Количество посещений за месяц</h2>
          <MyBarChart data={props.visitToMonth} />
        </div>
      </div>
      <div className='containerForBarChart'>
        <div className='BarChartAndHeader'>
          <h2>Количество посещений за год</h2>
          <MyBarChart data={props.visitToYear} />
        </div>
      </div>
      <div className='containerForBarChart'>
        <div className='BarChartAndHeader'>
          <h2>Зарегистрированных пользователей</h2>
          <MyAreaLine data={data} />
        </div>
      </div>
    </div>
  );
}

export default Dashboard;