// src/JobHeader.jsx

import React, { useEffect, useState } from 'react';
import JobDetails from './JobDetails.jsx';



const JobHeader = () => {
const [data, setData] = useState(null);

  useEffect(() => {
    const fetchJobs = async () => {
        const response = await fetch('http://localhost:5223/api/jobs'); // Replace with your API endpoint       
        const result = await response.json();
        setData(result.value);      
    };
    fetchJobs();
  }, []);

  if (!data) {
    return <div>Loading...</div>;
  }

  return (
    <>
    <div className="border-b pb-4 mb-4">
      <h1 className="text-3xl font-bold text-gray-900">{data.title}</h1>
      <h2 className="text-xl text-gray-600 mt-2">{data.company}</h2>
      <div className="flex items-center text-gray-500 mt-2">
        <span className="flex items-center mr-4">
          <svg xmlns="http://www.w3.org/2000/svg" className="h-4 w-4 mr-1" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M17.657 16.657L13.414 20.9a1.998 1.998 0 01-2.827 0l-4.244-4.243a8 8 0 1111.314 0z" />
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M15 11a3 3 0 11-6 0 3 3 0 016 0z" />
          </svg>
          {data.location}
        </span>
        <span className="flex items-center">
          <svg xmlns="http://www.w3.org/2000/svg" className="h-4 w-4 mr-1" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z" />
          </svg>
          Posted {data.postedDate}
        </span>
      </div>
      <JobDetails job={data} />
    </div>
    </>
  );
};

export default JobHeader;
