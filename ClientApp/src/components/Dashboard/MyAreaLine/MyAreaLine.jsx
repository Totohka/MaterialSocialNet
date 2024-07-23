import React, { PureComponent } from 'react';
import { AreaChart, Area, XAxis, YAxis, CartesianGrid, Tooltip, ResponsiveContainer } from 'recharts';

const MyAreaLine = (props) => {
    return (
        <ResponsiveContainer width="100%" height={400}>
            <AreaChart
            width={660}
            height={300}
            data={props.data}
            margin={{
                top: 10,
                right: 30,
                left: 0,
                bottom: 0,
            }}
            >
            <CartesianGrid strokeDasharray="3 0" />
            <XAxis dataKey="name" />
            <YAxis />
            <Tooltip />
            <Area type="monotone" dataKey="value" stroke="#8884d8" fill="#8884d8" />
            </AreaChart>
        </ResponsiveContainer>
    );
}
    
export default MyAreaLine;