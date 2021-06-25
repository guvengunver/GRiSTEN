function [G, O, DLeft, DRight] = GRiS(x)
% A function to implement GRiS following the paper:
% MEHMET GUVEN GUNVER, et al., "TO DETERMINE SKEWNESS, MEAN AND DEVIATION WITH A NEW APPROACH ON
% CONTINUOUS DATA" International Journal of Science an Research v 74, n
% 2/1, February 2018.
% This function is a brute force implementation using lots of high level
% Matlab fucntions. It is not optimized for speed. That said, data sets of
% 100,000 elements ran in negligible time on a Windows 10 machine with
% Intel Core I5-2400 @ 3.1 GHz with 8 GB of RAM.
% Michael A. Pusateri, 7/20/2018

% Some very basic error checking.
if (~isvector(x))
   error('Error: x must be a vector input not a matrix.')
elseif (isrow(x))
    error('Error: x must be a column vector.')
elseif (~isreal(x))
    error('Error: x must contain real numbers.')
end

% Compute some values used to find the skew and GRiS mean.
N = max(size(x));         % Elements in the data set.
med = median(x);          % Data median
sortedX = sort(x);        % Data sorted in ascending order
normX = sortedX - med;    % Data normalized by the median.
k4neg = find(normX<0);    % Indices for data below the median
k4pos = find(normX >= 0); % Indices for data at or above the median

% Compute the skew.  Page 67 of the article.
G = sum(normX(k4neg))/sum(normX(k4pos)); 

gr = (1 + sqrt(5))/2; % The Golden Ratio

% Compute the dynamic mean coefficient mask. Page 69 of the article.
Mci = [1/gr + 2*(k4neg-1)./(N-1); 1+gr - 2*(k4pos-1)./(N-1)];

% Compute the GRiS mean. Page 70 of the article.
O = med + sum(Mci.*normX)/sum(Mci);

% Compute some values needed for the left and right deviations.
normX = sortedX - O;      % Data normalized by the GRiS mean.
k4neg = find(normX < 0);  % Indices for data below the GRiS mean.
K = max(size(k4neg));     % Number of elements below the GRiS mean.
k4pos = find(normX >= 0); % Indices for data at or above the GRiS mean.

% Compute the dynamic deviation coefficient mask. Page 71 of the article.
Dci = gr - [(k4neg - 1)./(K - 1); 1 - (k4pos - K - 1)./(N - K - 1)];

% Compute the GRiS left and right deviation. Page 72 of the article.
DLeft = sum(Dci(k4neg) .* normX(k4neg))/sum(Dci(k4neg));
DRight = sum(Dci(k4pos) .* normX(k4pos))/sum(Dci(k4pos));




