#pragma once
#include <functional>
#include <chrono>
template<typename TFunc, typename TReturn>
TReturn timeOperation(TFunc &operation, TReturn) {

	auto start = std::chrono::steady_clock::now();
	TReturn result = operation();
	auto end = std::chrono::steady_clock::now();
	std::chrono::duration<double> diff = end - start;
	std::cout << "- took: " << diff.count() << "s" << std::endl;
	return result;
}