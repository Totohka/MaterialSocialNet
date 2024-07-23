import React from "react";
import {
  BarChart,
  Bar,
  Brush,
  ReferenceLine,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  Legend,
  ResponsiveContainer
} from "recharts";

const MyBarChart = (props) => {
  return (
    <ResponsiveContainer width="100%" height={400}>
        <BarChart
            width={660}
            height={300}
            data={props.data}
            margin={{
                top: 5,
                right: 30,
                left: 20,
                bottom: 5
            }}
            >
            <CartesianGrid strokeDasharray="1 0" />
            <XAxis dataKey="name" />
            <YAxis />
            <Tooltip />
            <ReferenceLine y={0} stroke="#900210" />
            <Brush dataKey="name" height={30} stroke="#8884d8" />
            <Bar dataKey="value" fill="#82ca9d" />
        </BarChart>
    </ResponsiveContainer>
  );
}

export default MyBarChart;